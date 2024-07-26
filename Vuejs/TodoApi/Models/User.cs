using System.ComponentModel.DataAnnotations;
public class User{
    [Key]
    public int UserId{get; set;}
    public string UserName{get; set;}
    public string UserEmail{get;set;}
    public string UserPassword{get ; set;}
    public string UserPhone{get; set;}
    public int OTP{get; set;} = 0;

}