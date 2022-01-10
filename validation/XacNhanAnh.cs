using System;
using System.ComponentModel.DataAnnotations;
public class XacNhanAnh :ValidationAttribute{
    public XacNhanAnh(){
        ErrorMessage = "Địa chỉ không phù hợp là ảnh";
    }
    public override bool IsValid(object value)
    {
        if(value == null){
            return false;
        }
        string temp = Convert.ToString(value);
        temp = temp.Trim();
        string Hash = temp.Substring(temp.Length-4);
        if(Hash.Equals(".jpg")){
            return true;
        }
        return false;
    }

}