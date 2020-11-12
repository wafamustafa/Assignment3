using System;
using System.Reflection;

namespace Assignment3_N01450753_WafaMustafa_5101B.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}