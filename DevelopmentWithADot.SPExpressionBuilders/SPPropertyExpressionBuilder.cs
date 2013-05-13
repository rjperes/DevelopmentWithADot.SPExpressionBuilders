using System;
using System.CodeDom;
using System.Web;
using System.Web.Compilation;
using System.Web.UI;
using Microsoft.Office.Server.UserProfiles;
using Microsoft.SharePoint;

namespace DevelopmentWithADot.SPExpressionBuilders
{
	[ExpressionPrefix("SPProperty")]
	public sealed class SPPropertyExpressionBuilder : ConvertedExpressionBuilder
	{
		#region Public static methods
		public static Object GetPropertyValue(String propertyName, Type propertyType)
		{
			SPServiceContext serviceContext = SPServiceContext.GetContext(HttpContext.Current);
			UserProfileManager upm = new UserProfileManager(serviceContext);
			UserProfile up = upm.GetUserProfile(false);
			Object propertyValue = (up[propertyName] != null) ? up[propertyName].Value : null;

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
			return (GetPropertyValue(entry.Expression, entry.PropertyInfo.PropertyType));
		}

		public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, Object parsedData, ExpressionBuilderContext context)
		{
			if (String.IsNullOrEmpty(entry.Expression) == true)
			{
				return (new CodePrimitiveExpression(String.Empty));
			}
			else
			{
				return (new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(this.GetType()), "GetPropertyValue"), new CodePrimitiveExpression(entry.Expression), new CodeTypeOfExpression(entry.PropertyInfo.PropertyType)));
			}
		}
		#endregion
	}
}
