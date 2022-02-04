using System;
using System.ComponentModel.DataAnnotations;
public class Money : ValidationAttribute
{
    public Money()
    {
        ErrorMessage ="Phải là Tiền";
    }
    public override bool IsValid(object value)
    {
        if(value == null){
            return false;
        }
        var temp =Int32.Parse(value.ToString());
        if(temp>=0 && temp%1000 == 0){
            return true;
        }
        return false;
    }
}