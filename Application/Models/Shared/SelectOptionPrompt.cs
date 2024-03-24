using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradingAppMvc.Domain.Utils;

namespace TradingAppMvc.Application.Models.Shared
{
    public class SelectOptionPrompt
    {
        public SelectOptionPrompt(string value, string? displayValue = default)
        {
            Value = value;
            DisplayValue = value;
            if (displayValue != null)
            {
                DisplayValue = displayValue;
            }
        }

        public SelectOptionPrompt() { }

        public bool Disabled { get; set; }
        public bool Selected { get; set; }
        public string? Value { get; set; }
        public string? DisplayValue { get; set; }

        public string SetDisplayValue(string value)
        {
            return char.ToUpper(value[0]) + value.Substring(1).ToLower();
        }

        public SelectOptionPrompt SetFormat(FormatTool.FormatType formatType){
            switch(formatType){
                case FormatTool.FormatType.UpperCase:
                DisplayValue = DisplayValue?.ToUpper();
                break;
                case FormatTool.FormatType.LowerCase:
                DisplayValue = DisplayValue?.ToLower();
                break;
                case FormatTool.FormatType.SnakeCase:
                DisplayValue = FormatTool.ToSnakeCase(DisplayValue ?? "");
                break;
                case FormatTool.FormatType.TitleCase:
                DisplayValue = FormatTool.ToTitleCase(DisplayValue ?? "");
                break;
            }
            return this;
        }
        public SelectOptionPrompt SetPlaceholder(string placeholder)
        {
            Disabled = true;
            Selected = true;
            DisplayValue = placeholder;
            return this;
        }

        public SelectOptionPrompt SetSelected(){
            Selected = true;
            return this;
        }
    }
}