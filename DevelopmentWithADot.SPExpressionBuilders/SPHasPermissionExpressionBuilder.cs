using System;
using System.CodeDom;
using System.Web.Compilation;
using System.Web.UI;
using Microsoft.SharePoint;

namespace DevelopmentWithADot.SPExpressionBuilders
{
	[ExpressionPrefix("SPHasPermission")]
	public sealed class SPHasPermissionExpressionBuilder : ExpressionBuilder
	{
		#region Public static methods
		public static Boolean HasPermission(String permissions)
		{
			foreach (var permissionGroup in permissions.Split(','))
			{
				var all = true;

				foreach (var permission in permissionGroup.Split('+'))
				{
					var perm = (SPBasePermissions)Enum.Parse(typeof(SPBasePermissions), permission, true);

					if (SPContext.Current.ListItem.DoesUserHavePermissions(perm) == false)
					{
						all = false;
						break;
					}
				}

				if (all == true)
				{
					return (true);
				}
			}

			return (false);
		}

		#endregion

		#region Public override methods
		public override Object EvaluateExpression(Object target, BoundPropertyEntry entry, Object parsedData, ExpressionBuilderContext context)
		{
			return (HasPermission(entry.Expression));
		}

		public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, Object parsedData, ExpressionBuilderContext context)
		{
			if (String.IsNullOrEmpty(entry.Expression) == true)
			{
				return (new CodePrimitiveExpression(String.Empty));
			}
			else
			{
				return (new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(this.GetType()), "HasPermission"), new CodePrimitiveExpression(entry.Expression)));
			}
		}
		#endregion
	}
}
