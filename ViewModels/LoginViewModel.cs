using System.ComponentModel.DataAnnotations;

namespace Pos.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "請輸入帳號")]
    [Display(Name = "帳號")]
    public string UserId { get; set; } = default!;

    [Required(ErrorMessage = "請輸入密碼")]
    [DataType(DataType.Password)]
    [Display(Name = "密碼")]
    public string Password { get; set; } = default!;
}

public class ChangePasswordViewModel
{
    [Required(ErrorMessage = "請輸入舊密碼")]
    [DataType(DataType.Password)]
    [Display(Name = "舊密碼")]
    public string OldPassword { get; set; } = default!;

    [Required(ErrorMessage = "請輸入新密碼")]
    [DataType(DataType.Password)]
    [Display(Name = "新密碼")]
    public string NewPassword { get; set; } = default!;

    [Required(ErrorMessage = "請確認新密碼")]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "兩次密碼不一致")]
    [Display(Name = "確認新密碼")]
    public string ConfirmPassword { get; set; } = default!;
}
