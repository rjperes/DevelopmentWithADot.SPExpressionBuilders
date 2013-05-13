using System;
using System.CodeDom;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace DevelopmentWithADot.SPExpressionBuilders
{
	[ExpressionPrefix("SPIsInGroup")]
	public sealed class SPIsInGroupExpressionBuilder : ExpressionBuilder
	{
		#region Public static methods
		public static Boolean IsInGroup(String groupNames)
		{
			foreach (String groupGroup in groupNames.Split(','))
			{
				Boolean all = true;

				foreach (String groupName in groupGroup.Split('+'))
				{
					if (SPControl.GetContextWeb(HttpContext.Current).CurrentUser.Groups.OfType<SPGroup>().Any(x => String.Equals(x.Name, groupName, StringComparison.OrdinalIgnoreCase)) == false)
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

		public override Boolean SupportsEvaluate
		{
			get
			{
				return (true);
			}
		}

		#region Public override methods
		public override Object EvaluateExpression(Object target, BoundPropertyEntry entry, Object parsedData, ExpressionBuilderContext context)
		{
			return (IsInGroup(entry.Expression));
		}

		public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, Object parsedData, ExpressionBuilderContext context)
		{
			if (String.IsNullOrEmpty(entry.Expression) == true)
			{
				return (new CodePrimitiveExpression(String.Empty));
			}
			else
			{
				return (new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(this.GetType()), "IsInGroup"), new CodePrimitiveExpression(entry.Expression)));
			}
		}
		#endregion
	}
}
