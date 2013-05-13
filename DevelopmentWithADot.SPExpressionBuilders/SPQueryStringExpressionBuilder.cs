using System;
using System.CodeDom;
using System.Web;
using System.Web.Compilation;
using System.Web.UI;

namespace DevelopmentWithADot.SPExpressionBuilders
{
	[ExpressionPrefix("SPQueryString")]
	public sealed class SPQueryStringExpressionBuilder : ConvertedExpressionBuilder
	{
		#region Public static methods
		public static Object GetQueryStringValue(String parameterName, Type propertyType)
		{
			Object parameterValue = HttpContext.Current.Request.QueryString[parameterName];

			return (Convert(parameterValue, propertyType));
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
			return (GetQueryStringValue(entry.Expression, entry.PropertyInfo.PropertyType));
		}

		public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, Object parsedData, ExpressionBuilderContext context)
		{
			if (String.IsNullOrEmpty(entry.Expression) == true)
			{
				return (new CodePrimitiveExpression(String.Empty));
			}
			else
			{
				return (new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(this.GetType()), "GetQueryStringValue"), new CodePrimitiveExpression(entry.Expression), new CodeTypeOfExpression(entry.PropertyInfo.PropertyType)));
			}
		}
		#endregion
	}
}
