﻿using System.Reflection;
using MigraDoc.DocumentObjectModel;
using PdfSharp.Fonts;

namespace CoBudget.Application.UseCases.Expenses.Reports.Pdf.Fonts;

public class ExpensesReportFontResolver : IFontResolver
{
    public byte[]? GetFont(string faceName)
    {
        var stream = ReadFontFile(faceName) ?? ReadFontFile(FontHelper.DEFAULT_FONT);

        var lenght = (int)stream!.Length;
        var data = new byte[lenght];

        stream.Read(buffer: data, offset: 0, count: lenght);

        return data;
    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo(familyName);    
    }
        
    private static Stream? ReadFontFile(string faceName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return assembly.GetManifestResourceStream($"CoBudget.Application.UseCases.Expenses.Reports.Pdf.Fonts.{faceName}.ttf");
    }
}
