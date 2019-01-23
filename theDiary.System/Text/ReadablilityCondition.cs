using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Text
{
    /// <summary>
    /// A bitwise flag used to indicate the conditions to use when performing normalization.
    /// </summary>
    [Flags]
    public enum ReadablilityCondition
        : byte
    {
        /// <summary>
        /// Specifies that Normalization should not happen if the value contains any whitespace.
        /// <remarks>If specified with <value>TrimLeadingWhiteSpace</value> or <value>TrimTrailingWhiteSpace</value> then any leading or trailing
        /// whitespace will be removed prior to checking for this flag.</remarks>
        /// </summary>
        StopIfAnyWhitespace = 1,
        
        /// <summary>
        /// Specifies that the value to be normalized should have any leading whitespace removed, prior to normalization.
        /// </summary>
        TrimLeadingWhiteSpace = 2,

        /// <summary>
        /// Specifies that the value to be normalized should have any trailing whitespace removed, prior to normalization.
        /// </summary>
        TrimTrailingWhiteSpace = 4,

        /// <summary>
        /// Specifies that the value to be normalized should have any leading and/or trailing whitespace removed, prior to normalization.
        /// </summary>
        TrimWhiteSpace = 6,

        /// <summary>
        /// Specifies that normalization should be performed on whether the Character is Upper or Lower case.
        /// </summary>
        ByCase = 8,

        /// <summary>
        /// Specifies that normalization should be performed on whether the Character is a Digit or not.
        /// </summary>
        ByDigit = 16,

        /// <summary>
        /// Specifies that normalization should be performed on whether the Character is an Underscore '_' character.
        /// </summary>
        ByUnderscore = 32,

        /// <summary>
        /// Specifies that normalization should make the first Character Upper case if it is not.
        /// </summary>
        CapitalizeFirstCharacter = 64,

        /// <summary>
        /// Specifies the default normalization conditions.
        /// </summary>
        Default = 126
    }
}
