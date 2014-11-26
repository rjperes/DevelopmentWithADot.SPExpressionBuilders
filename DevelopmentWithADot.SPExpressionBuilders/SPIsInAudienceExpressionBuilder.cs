using System;
using System.CodeDom;
using System.Web;
using System.Web.Compilation;
using System.Web.UI;
using Microsoft.Office.Server.Audience;
using Microsoft.SharePoint.WebControls;

namespace DevelopmentWithADot.SPExpressionBuilders
{
	[ExpressionPrefix("SPIsInAudience")]
	public sealed class SPIsInAudienceExpressionBuilder : ExpressionBuilder
	{
		#region Public static methods
		public static Boolean IsInAudience(String audienceNames)
		{
			var manager = new AudienceManager();

			foreach (var audienceGroup in audienceNames.Split(','))
			{
				var all = true;

				foreach (String audienceName in audienceGroup.Split('+'))
				{
					if (manager.IsMemberOfAudience(SPControl.GetContextWeb(HttpContext.Current).CurrentUser.LoginName, audienceName) == false)
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
			return (IsInAudience(entry.Expression));
		}

		public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, Object parsedData, ExpressionBuilderContext context)
		{
			if (String.IsNullOrEmpty(entry.Expression) == true)
			{
				return (new CodePrimitiveExpression(String.Empty));
			}
			else
			{
				return (new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(this.GetType()), "IsInAudience"), new CodePrimitiveExpression(entry.Expression)));
			}
		}
		#endregion
	}
}
