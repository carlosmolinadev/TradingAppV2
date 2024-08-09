using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TradingAppMvc.Models.Shared
{
   public class Prompt
   {
      public string Id { get; private set; }
      public string Name { get; private set; }
      public PromptType Type { get; private set; }
      public string Label { get; private set; }
      public bool Required { get; private set; }
      public bool LoadingIndicator { get; private set; }
      public string Pattern { get; private set; }
      public string PlaceHolder { get; private set; }
      public string HtmxValue { get; private set; }
      public string SelectDisplayValue { get; private set; }
      public string ValidationError { get; private set; }
      public string ErrorMessage { get; private set; }
      public List<SelectListItem> Options { get; private set; }
      public Dictionary<string, object> Attributes { get; private set; } = new();

      public void SetSelectOption(IEnumerable<SelectListItem> options)
      {
         Options = options.ToList();
      }

      public void SetErrorMessage(string error)
      {
         ErrorMessage = error;
      }

      public static Prompt CreateTextPrompt(string id, string name)
      {
         return new Prompt() { Id = id, Name = name, Type = PromptType.Text };
      }

      public static Prompt CreateSelectPrompt(string id, string name, IEnumerable<SelectListItem> options)
      {
         return new Prompt() { Id = id, Name = name, Type = PromptType.Select, Options = options.ToList() };
      }

      public Prompt SetHidden()
      {
         Attributes.Add("hidden", true);
         return this;
      }
      public Prompt SetDisabled()
      {
         Attributes.Add("disabled", true);
         return this;
      }
      public Prompt SetRequired()
      {
         Required = true;
         return this;
      }
      public Prompt SetPlaceholder(string placeholder)
      {
         if (Options.Any())
         {
            Options.Insert(0, new SelectListItem());
         }
         PlaceHolder = placeholder;
         return this;
      }

      public Prompt SetLoadingIndicator()
      {
         LoadingIndicator = true;
         return this;
      }

      public Prompt SetLabel(string label)
      {
         Label = label;
         var type = Type;
         if (type == PromptType.Text)
         {
            Type = PromptType.LabelText;
         }
         if (type == PromptType.Select)
         {
            Type = PromptType.LabelSelect;
         }
         return this;
      }
      public Prompt SetPattern(string pattern)
      {
         Pattern = pattern;
         return this;
      }
      public Prompt SetReadonly()
      {
         Attributes.Add("readonly", true);
         return this;
      }
      public Prompt SetCustomAttribute(string attribute, string value)
      {
         Attributes.Add(attribute, value);
         return this;
      }


      public enum PromptType
      {
         Text,
         LabelText,
         Select,
         LabelSelect,

      }
   }
}