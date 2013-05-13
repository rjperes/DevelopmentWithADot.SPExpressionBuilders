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
			foreach (String permissionGroup in permissions.Split(','))
			{
				Boolean all = true;

				foreach (String permission in permissionGroup.Split('+'))
				{
					SPBasePermissions perm = (SPBasePermissions)Enum.Parse(typeof(SPBasePermissions), permission, true);

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
