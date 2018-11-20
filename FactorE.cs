using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

//{TODO} Do we really need this class?
namespace Excessives {
	//Is meant to be a factory, where you don't pass a string, but rather the type
	public class FactorE<TValue> {
		static List<Type> affectorList = new List<Type>();

		public void AddItem(Type type) {
			if (!type.IsSubclassOf(typeof(TValue)))
				throw new Exception($"The type passed: '{type.FullName}' does not inherit from '{typeof(TValue).FullName}'");

			if (!affectorList.Contains(type))
				affectorList.Add(type);
		}

		public void RemoveItem(Type type) {
			if (!type.IsSubclassOf(typeof(TValue)))
				throw new Exception($"The type passed: '{type.FullName}' does not inherit from '{typeof(TValue).FullName}'");

			if (affectorList.Contains(type))
				affectorList.Remove(type);

		}

		//... This function is massive...
		/// <summary>
		/// Creates a new instance given the desired type
		/// </summary>
		/// <param name="requestedType"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public TValue GetNewInstance(Type requestedType, params object[] parameters) {
			if (!requestedType.IsSubclassOf(typeof(TValue)))
				throw new Exception($"The type passed: '{requestedType.FullName}' does not inherit from '{typeof(TValue).FullName}'");

			if (affectorList.Contains(requestedType)) {
				//Will correctly find the correct constructor to call, given the parameters
				ConstructorInfo[] constructors = requestedType.GetConstructors();

				ParameterInfo[] parameterInfo; //Initialize this once

				bool correctType = true;

				for (int i = 0; i < constructors.Length; i++) {
					parameterInfo = constructors[i].GetParameters(); //Just so we don't try to find the parameters multiple times
					if (parameterInfo.Length != parameters.Length) //Must have the same number of parameters
						continue;
					correctType = true;
					for (int j = 0; j < parameterInfo.Length; j++) //Now check that all parameters are the same type
					{
						if (parameters[j].GetType() != parameterInfo[j].GetType()) {
							correctType = false;
							break;
						}

						if (correctType) {
							return (TValue)constructors[i].Invoke(parameters);
						}
					}
				}

				//Could not find correct constructor
				throw new Exception($"The parameters passed did not fit any constructors for the type: {requestedType.FullName}");
			} else {
				return default(TValue);
			}
		}
	}
}
