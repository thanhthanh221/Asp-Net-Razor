using System;
using System.ComponentModel.DataAnnotations;
public class XacNhanTuoi : ValidationAttribute
{
    public XacNhanTuoi()
    {
        ErrorMessage ="Tuổi Không phù hợp";
    }
    public override bool IsValid(object value)
    {
        if(value == null){
            return false;
        }
        var temp =DateTime.Parse(value.ToString());
        if(temp.Year<=2007){
            return true;
        }
        return false;
    }
}