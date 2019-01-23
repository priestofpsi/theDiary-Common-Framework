using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Specifies that a data field is required only on certain conditions.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class RequiredIfAttribute 
        : ValidationAttribute
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfAttribute"/> class.
        /// </summary>
        /// <param name="condition">A string value containing the Lambda expression to evaluate.</param>
        public RequiredIfAttribute(string condition)
        {
            if (condition.IsNullEmptyOrWhiteSpace())
                throw new ArgumentNullException("condition");

            this.condition = condition;
        }
        #endregion

        #region Private Declarations
        private readonly string condition;
        #endregion

        #region Protected Override Methods & Functions
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Delegate conditionFunction = this.CreateExpression(validationContext.ObjectType, this.condition);
            bool conditionMet = (bool)conditionFunction.DynamicInvoke(validationContext.ObjectInstance);
            if (conditionMet && value == null)
                    return new ValidationResult(FormatErrorMessage(null));

            return null;
        }
        #endregion

        #region Private Methods & Functions
        private Delegate CreateExpression(Type objectType, string expression)
        {
            // TODO - add caching
            LambdaExpression lambdaExpression = System.Linq.Dynamic.DynamicExpression.ParseLambda(objectType, typeof(bool), expression);
            Delegate func = lambdaExpression.Compile();

            return func;
        }
        #endregion
    }
}
