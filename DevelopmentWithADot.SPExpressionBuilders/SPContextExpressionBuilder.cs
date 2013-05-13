using System;
using System.CodeDom;
using System.Web.Compilation;
using System.Web.UI;
using Microsoft.SharePoint;

namespace DevelopmentWithADot.SPExpressionBuilders
{
	[ExpressionPrefix("SPContext")]
	public sealed class SPContextExpressionBuilder : ConvertedExpressionBuilder
	{
		#region Public static methods
		public static Object GetContextValue(String expression, Type propertyType)
		{
			SPContext context = SPContext.Current;
			Object expressionValue = DataBinder.Eval(context, expression.Trim().Replace('\'', '"'));

			return (Convert(expressionValue, propertyType));
		}

		#endregion

		#region Public override methods
		public override Object EvaluateExpression(Object target, BoundPropertyEntry entry, Object parsedData, ExpressionBuilderContext context)
		{
			return (GetContextValue(entry.Expression, entry.PropertyInfo.PropertyType));
		}

		public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, Object parsedData, ExpressionBuilderContext context)
		{
			if (String.IsNullOrEmpty(entry.Expression) == true)
			{
				return (new CodePrimitiveExpression(String.Empty));
			}
			else
			{
				return (new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(this.GetType()), "GetContextValue"), new CodePrimitiveExpression(entry.Expression), new CodeTypeOfExpression(entry.PropertyInfo.PropertyType)));
			}
		}
		#endregion
	}
}
