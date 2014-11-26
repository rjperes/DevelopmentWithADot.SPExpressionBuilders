using System;
using System.Web;
using System.Web.Compilation;
using System.Web.UI;
using Microsoft.Office.Server.UserProfiles;
using Microsoft.SharePoint;

namespace DevelopmentWithADot.SPExpressionBuilders
{
	[ExpressionPrefix("SPProfileProperty")]
	public sealed class SPProfilePropertyExpressionBuilder : ConvertedExpressionBuilder
	{
		#region Public static methods
		public static Object GetValue(String propertyName, Type propertyType)
		{
			var serviceContext = SPServiceContext.GetContext(HttpContext.Current);
			var upm = new UserProfileManager(serviceContext);
			var up = upm.GetUserProfile(false);
			var propertyValue = (up[propertyName] != null) ? up[propertyName].Value : null;

			return (Convert(propertyValue, propertyType));
		}

		#endregion

		#region Public override methods
		public override Object EvaluateExpression(Object target, BoundPropertyEntry entry, Object parsedData, ExpressionBuilderContext context)
		{
			return (GetValue(entry.Expression, entry.PropertyInfo.PropertyType));
		}

		#endregion
	}
}
