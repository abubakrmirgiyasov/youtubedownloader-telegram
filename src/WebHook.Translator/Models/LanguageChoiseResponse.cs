﻿#nullable disable

using WebHook.Translator.Common;

namespace WebHook.Translator.Models;

public class LanguageChoiseResponse
{
    public int MessageId { get; set; }

    public string Code { get; set; }

    public KeyboardDirection Direction { get; set; }
}
