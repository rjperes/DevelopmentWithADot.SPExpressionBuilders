using System;
using System.CodeDom;
using System.Web.Compilation;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Publishing;

namespace DevelopmentWithADot.SPExpressionBuilders
{
	[ExpressionPrefix("SPPublishingPage")]
	public sealed class SPPublishingPageExpressionBuilder : ConvertedExpressionBuilder
	{
		#region Public static methods
		public static Object GetPublishingPageValue(String expression, Type propertyType)
		{
			PublishingPage page = PublishingPage.GetPublishingPage(SPContext.Current.ListItem);
			Object expressionValue = DataBinder.Eval(page, expression.Trim().Replace('\'', '"'));

			return (Convert(expressionValue, propertyType));
		}

		#endregion

		#region Public override properties
		public override Boolean SupportsEvaluate
		{
			get
			{
				return (true);
			}
		}
		#endregion

		#region Public override methods
		public override Object EvaluateExpression(Object target, BoundPropertyEntry entry, Object parsedData, ExpressionBuilderContext context)
		{
			return (GetPublishingPageValue(entry.Expression, entry.PropertyInfo.PropertyType));
		}

		public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, Object parsedData, ExpressionBuilderContext context)
		{
			if (String.IsNullOrEmpty(entry.Expression) == true)
			{
				return (new CodePrimitiveExpression(String.Empty));
			}
			else
			{
				return (new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(this.GetType()), "GetPublishingPageValue"), new CodePrimitiveExpression(entry.Expression), new CodeTypeOfExpression(entry.PropertyInfo.PropertyType)));
			}
		}
		#endregion
	}
}
