using System;
using System.CodeDom;
using System.Web.Compilation;
using System.Web.UI;
using Microsoft.SharePoint.Administration;

namespace DevelopmentWithADot.SPExpressionBuilders
{
	[ExpressionPrefix("SPFarmProperty")]
	public sealed class SPFarmPropertyExpressionBuilder : ConvertedExpressionBuilder
	{
		#region Public static methods
		public static Object GetFarmPropertyValue(String propertyName, Type propertyType)
		{
			Object propertyValue = SPFarm.Local.Properties[propertyName];

			return (Convert(propertyValue, propertyType));
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
			return (GetFarmPropertyValue(entry.Expression, entry.PropertyInfo.PropertyType));
		}

		public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, Object parsedData, ExpressionBuilderContext context)
		{
			if (String.IsNullOrEmpty(entry.Expression) == true)
			{
				return (new CodePrimitiveExpression(String.Empty));
			}
			else
			{
				return (new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(this.GetType()), "GetFarmPropertyValue"), new CodePrimitiveExpression(entry.Expression), new CodeTypeOfExpression(entry.PropertyInfo.PropertyType)));
			}
		}
		#endregion
	}
}
