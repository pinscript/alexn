// Taken from https://raw.github.com/NancyFx/Nancy/master/src/Nancy/DynamicDictionary.cs
// THANKS!

using System.ComponentModel;
using System.Linq.Expressions;
using Microsoft.CSharp.RuntimeBinder;

namespace alexn {
    using System;
    using System.Collections.Generic;
    using System.Dynamic;

    public class DynamicDictionary : DynamicObject, IEquatable<DynamicDictionary>, IHideObjectMembers {
        private readonly Dictionary<string, object> dictionary = new Dictionary<string, object>();

        public static DynamicDictionary Empty {
            get {
                return new DynamicDictionary();
            }
        }

        /// <summary>
        /// Provides the implementation for operations that set member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject"/> class can override this method to specify dynamic behavior for operations such as setting a value for a property.
        /// </summary>
        /// <returns>true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)</returns>
        /// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member to which the value is being assigned. For example, for the statement sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject"/> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param><param name="value">The value to set to the member. For example, for sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject"/> class, the <paramref name="value"/> is "Test".</param>
        public override bool TrySetMember(SetMemberBinder binder, object value) {
            this[binder.Name] = value;
            return true;
        }

        /// <summary>
        /// Provides the implementation for operations that get member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject"/> class can override this method to specify dynamic behavior for operations such as getting a value for a property.
        /// </summary>
        /// <returns>true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a run-time exception is thrown.)</returns>
        /// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member on which the dynamic operation is performed. For example, for the Console.WriteLine(sampleObject.SampleProperty) statement, where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject"/> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param><param name="result">The result of the get operation. For example, if the method is called for a property, you can assign the property value to <paramref name="result"/>.</param>
        public override bool TryGetMember(GetMemberBinder binder, out object result) {
            if (!dictionary.TryGetValue(binder.Name, out result)) {
                result = new DynamicDictionaryValue(null);
            }

            return true;
        }

        /// <summary>
        /// Returns the enumeration of all dynamic member names. </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> that contains dynamic member names.</returns>
        public override IEnumerable<string> GetDynamicMemberNames() {
            return dictionary.Keys;
        }

        /// <summary>
        /// Gets or sets the <see cref="DynamicDictionaryValue"/> with the specified name.
        /// </summary>
        /// <value>A <see cref="DynamicDictionaryValue"/> instance containing a value.</value>
        public dynamic this[string name] {
            get {
                dynamic member;
                if (!dictionary.TryGetValue(name, out member)) {
                    member = new DynamicDictionaryValue(null);
                }

                return member;
            }
            set { dictionary[name] = value is DynamicDictionaryValue ? value : new DynamicDictionaryValue(value); }
        }

        /// <summary>
        /// Indicates whether the current <see cref="DynamicDictionary"/> is equal to another object of the same type.
        /// </summary>
        /// <returns><see langword="true"/> if the current instance is equal to the <paramref name="other"/> parameter; otherwise, <see langword="false"/>.</returns>
        /// <param name="other">An <see cref="DynamicDictionary"/> instance to compare with this instance.</param>
        public bool Equals(DynamicDictionary other) {
            if (ReferenceEquals(null, other)) {
                return false;
            }

            return ReferenceEquals(this, other) || Equals(other.dictionary, this.dictionary);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns><see langword="true"/> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }

            if (ReferenceEquals(this, obj)) {
                return true;
            }

            return obj.GetType() == typeof(DynamicDictionary) && this.Equals((DynamicDictionary)obj);
        }

        /// <summary>
        /// Returns a hash code for this <see cref="DynamicDictionary"/>.
        /// </summary>
        /// <returns> A hash code for this <see cref="DynamicDictionary"/>, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode() {
            return (dictionary != null ? dictionary.GetHashCode() : 0);
        }
    }

    public class DynamicDictionaryValue : DynamicObject, IEquatable<DynamicDictionaryValue>, IHideObjectMembers
    {
        private readonly object value;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicDictionaryValue"/> class.
        /// </summary>
        /// <param name="value">The value to store in the instance</param>
        public DynamicDictionaryValue(object value)
        {
            this.value = value;
        }

        /// <summary>
        /// Gets a value indicating whether this instance has value.
        /// </summary>
        /// <value><c>true</c> if this instance has value; otherwise, <c>false</c>.</value>
        /// <remarks><see langword="null"/> is considered as not being a value.</remarks>
        public bool HasValue
        {
            get { return (this.value != null); }
        }

        /// <summary>
        /// Gets the inner value
        /// </summary>
        public object Value
        {
            get { return this.value; }
        }

        public static bool operator ==(DynamicDictionaryValue dynamicValue, object compareValue)
        {
            if (dynamicValue.value == null && compareValue == null)
            {
                return true;
            }

            return dynamicValue.value != null && dynamicValue.value.Equals(compareValue);
        }

        public static bool operator !=(DynamicDictionaryValue dynamicValue, object compareValue)
        {
            return !(dynamicValue == compareValue);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns><c>true</c> if the current object is equal to the <paramref name="compareValue"/> parameter; otherwise, <c>false</c>.
        /// </returns>
        /// <param name="compareValue">An <see cref="DynamicDictionaryValue"/> to compare with this instance.</param>
        public bool Equals(DynamicDictionaryValue compareValue)
        {
            if (ReferenceEquals(null, compareValue))
            {
                return false;
            }

            return ReferenceEquals(this, compareValue) || Equals(compareValue.value, this.value);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="object"/>.
        /// </summary>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current <see cref="DynamicDictionaryValue"/>; otherwise, <c>false</c>.</returns>
        /// <param name="compareValue">The <see cref="object"/> to compare with the current <see cref="DynamicDictionaryValue"/>.</param>
        public override bool Equals(object compareValue)
        {
            if (ReferenceEquals(null, compareValue))
            {
                return false;
            }

            if (ReferenceEquals(this, compareValue))
            {
                return true;
            }

            return compareValue.GetType() == typeof(DynamicDictionaryValue) && this.Equals((DynamicDictionaryValue)compareValue);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode()
        {
            return (this.value != null ? this.value.GetHashCode() : 0);
        }

        /// <summary>
        /// Provides implementation for binary operations. Classes derived from the <see cref="T:System.Dynamic.DynamicObject"/> class can override this method to specify dynamic behavior for operations such as addition and multiplication.
        /// </summary>
        /// <returns><c>true</c> if the operation is successful; otherwise, <c>false</c>. If this method returns <c>false</c>, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)</returns>
        /// <param name="binder">Provides information about the binary operation. The binder.Operation property returns an <see cref="T:System.Linq.Expressions.ExpressionType"/> object. For example, for the sum = first + second statement, where first and second are derived from the DynamicObject class, binder.Operation returns ExpressionType.Add.</param><param name="arg">The right operand for the binary operation. For example, for the sum = first + second statement, where first and second are derived from the DynamicObject class, <paramref name="arg"/> is equal to second.</param><param name="result">The result of the binary operation.</param>
        public override bool TryBinaryOperation(BinaryOperationBinder binder, object arg, out object result)
        {
            object resultOfCast;
            result = null;

            if (binder.Operation != ExpressionType.Equal)
            {
                return false;
            }

            var convert =
                Binder.Convert(CSharpBinderFlags.None, arg.GetType(), typeof(DynamicDictionaryValue));

            if (!TryConvert((ConvertBinder)convert, out resultOfCast))
            {
                return false;
            }

            result = (resultOfCast == null) ? 
                Equals(arg, resultOfCast) :
                resultOfCast.Equals(arg);

            return true;
        }

        /// <summary>
        /// Provides implementation for type conversion operations. Classes derived from the <see cref="T:System.Dynamic.DynamicObject"/> class can override this method to specify dynamic behavior for operations that convert an object from one type to another.
        /// </summary>
        /// <returns><c>true</c> if the operation is successful; otherwise, <c>false</c>. If this method returns <c>false</c>, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)</returns>
        /// <param name="binder">Provides information about the conversion operation. The binder.Type property provides the type to which the object must be converted. For example, for the statement (String)sampleObject in C# (CType(sampleObject, Type) in Visual Basic), where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject"/> class, binder.Type returns the <see cref="T:System.String"/> type. The binder.Explicit property provides information about the kind of conversion that occurs. It returns true for explicit conversion and false for implicit conversion.</param><param name="result">The result of the type conversion operation.</param>
        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            result = null;

            if (value == null)
            {
                return true;
            }

            var binderType = binder.Type;
            if (binderType == typeof(String))
            {
                result = Convert.ToString(value);
                return true;
            }

            if (binderType == typeof(Guid) || binderType == typeof(Guid?))
            {
                Guid guid;
                if (Guid.TryParse(Convert.ToString(value), out guid))
                {
                    result = guid;
                    return true;
                }
            }
            else if (binderType == typeof(TimeSpan) || binderType == typeof(TimeSpan?))
            {
                TimeSpan timespan;
                if (TimeSpan.TryParse(Convert.ToString(value), out timespan))
                {
                    result = timespan;
                    return true;
                }
            }
            else
            {
                if (binderType.IsGenericType && binderType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    binderType = binderType.GetGenericArguments()[0];
                }

                var typeCode = Type.GetTypeCode(binderType);

                if (typeCode == TypeCode.Object) // something went wrong here
                {
                    return false;
                }

                result = Convert.ChangeType(value, typeCode);

                return true;
            }
            return base.TryConvert(binder, out result);
        }

        public override string ToString()
        {
            return this.value == null ? base.ToString() : Convert.ToString(this.value);
        }

        public static implicit operator bool(DynamicDictionaryValue dynamicValue)
        {
            if (!dynamicValue.HasValue)
            {
                return false;
            }

            if (dynamicValue.value.GetType().IsValueType)
            {
                return (Convert.ToBoolean(dynamicValue.value));
            }

            bool result;
            if (bool.TryParse(dynamicValue.ToString(), out result))
            {
                return result;
            }

            return true;
        }

        public static implicit operator string(DynamicDictionaryValue dynamicValue)
        {
            return dynamicValue.ToString();
        }

        public static implicit operator int(DynamicDictionaryValue dynamicValue)
        {
            if (dynamicValue.value.GetType().IsValueType)
            {
                return Convert.ToInt32(dynamicValue.value);
            }

            return int.Parse(dynamicValue.ToString());
        }

        public static implicit operator Guid(DynamicDictionaryValue dynamicValue)
        {
            if (dynamicValue.value is Guid)
            {
                return (Guid)dynamicValue.value;
            }

            return Guid.Parse(dynamicValue.ToString());
        }

        public static implicit operator DateTime(DynamicDictionaryValue dynamicValue)
        {
            if (dynamicValue.value is DateTime)
            {
                return (DateTime)dynamicValue.value;
            }

            return DateTime.Parse(dynamicValue.ToString());
        }

        public static implicit operator TimeSpan(DynamicDictionaryValue dynamicValue)
        {
            if (dynamicValue.value is TimeSpan)
            {
                return (TimeSpan)dynamicValue.value;
            }

            return TimeSpan.Parse(dynamicValue.ToString());
        }

        public static implicit operator long(DynamicDictionaryValue dynamicValue)
        {
            if (dynamicValue.value.GetType().IsValueType)
            {
                return Convert.ToInt64(dynamicValue.value);
            }

            return long.Parse(dynamicValue.ToString());
        }

        public static implicit operator float(DynamicDictionaryValue dynamicValue)
        {
            if (dynamicValue.value.GetType().IsValueType)
            {
                return Convert.ToSingle(dynamicValue.value);
            }

            return float.Parse(dynamicValue.ToString());
        }

        public static implicit operator decimal(DynamicDictionaryValue dynamicValue)
        {
            if (dynamicValue.value.GetType().IsValueType)
            {
                return Convert.ToDecimal(dynamicValue.value);
            }

            return decimal.Parse(dynamicValue.ToString());
        }

        public static implicit operator double(DynamicDictionaryValue dynamicValue)
        {
            if (dynamicValue.value.GetType().IsValueType)
            {
                return Convert.ToDouble(dynamicValue.value);
            }

            return double.Parse(dynamicValue.ToString());
        }
    }

    /// <summary>
    /// Helper interface used to hide the base <see cref="Object"/>  members from the fluent API to make it much cleaner 
    /// in Visual Studio intellisense.
    /// </summary>
    /// <remarks>Created by Daniel Cazzulino http://www.clariusconsulting.net/blogs/kzu/archive/2008/03/10/58301.aspx</remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IHideObjectMembers
    {
        /// <summary>
        /// Hides the <see cref="Equals"/> method.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool Equals(object obj);

        /// <summary>
        /// Hides the <see cref="GetHashCode"/> method.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();

        /// <summary>
        /// Hides the <see cref="GetType"/> method.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        Type GetType();

        /// <summary>
        /// Hides the <see cref="ToString"/> method.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        string ToString();
    }
}