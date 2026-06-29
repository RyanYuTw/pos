#!/usr/bin/env zsh

PROJECT_DIR="$(cd "$(dirname "$0")" && pwd)"
PORT=8003
URL="http://localhost:$PORT"
MSSQL_CONTAINER="mssql_server"

echo "▶ 停止舊程序..."
pkill -f "dotnet.*Pos" 2>/dev/null || true
sleep 1

echo "▶ 確認 MSSQL 容器狀態..."
CONTAINER_STATUS=$(docker inspect -f '{{.State.Status}}' "$MSSQL_CONTAINER" 2>/dev/null)
if [ "$CONTAINER_STATUS" = "running" ]; then
  echo "   MSSQL 已在執行中"
elif [ "$CONTAINER_STATUS" = "exited" ] || [ "$CONTAINER_STATUS" = "created" ]; then
  echo "   啟動 MSSQL 容器..."
  docker start "$MSSQL_CONTAINER"
  echo "   等待 MSSQL 就緒（port 1433）..."
  for i in {1..30}; do
    nc -z localhost 1433 2>/dev/null && break
    sleep 2
  done
  sleep 2
  echo "   MSSQL 就緒"
else
  echo "❌ 找不到 MSSQL 容器（$MSSQL_CONTAINER），請先建立容器。"
  exit 1
fi

echo "▶ Rebuild 專案..."
cd "$PROJECT_DIR"
dotnet build --configuration Debug 2>&1
if [ $? -ne 0 ]; then
  echo "❌ Build 失敗，請檢查錯誤訊息。"
  exit 1
fi

echo "▶ 啟動應用程式（port $PORT）..."
ASPNETCORE_ENVIRONMENT=Development dotnet run --configuration Debug --no-build --no-launch-profile --urls "http://localhost:$PORT" &
APP_PID=$!

echo "▶ 等待網站啟動..."
for i in {1..30}; do
  if curl -s -o /dev/null -w "%{http_code}" "$URL" | grep -qE "^[23]"; then
    break
  fi
  sleep 1
done

echo "▶ 開啟瀏覽器：$URL"
open "$URL"

echo "✅ 完成！PID=$APP_PID，按 Ctrl+C 停止伺服器。"
wait $APP_PID
