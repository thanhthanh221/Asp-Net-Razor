using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
public class NhanVien_Name_Binding : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if(bindingContext == null){
            throw new AggregateException("bindingContext");
        }
        string modelName =bindingContext.ModelName;
        ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

        if(valueProviderResult == ValueProviderResult.None){
            return Task.CompletedTask;
        }
        else{
            string value = valueProviderResult.FirstValue;
            if(string.IsNullOrEmpty(value)){
                return Task.CompletedTask;
            }
            string s = value.ToUpper();
            s= s.Trim();
            bindingContext.ModelState.SetModelValue(modelName, s, s);
            bindingContext.Result = ModelBindingResult.Success(s);
            return Task.CompletedTask;
        }
    
    }
}