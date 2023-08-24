﻿#nullable disable

namespace WebHook.Translator.Models;

public class Play : IBase
{
    public string Type { get; set; }

    public string Ico { get; set; }

    public string Code { get; set; }

    public override string ToString()
    {
        return $"{Type}{Ico}";
    }
}
