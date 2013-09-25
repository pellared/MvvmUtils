//
//  This file is included when building a contract declarative assembly
//  in order to mark it as such for recognition by the tools
//

[assembly: global::System.Diagnostics.Contracts.ContractDeclarativeAssembly]

namespace System.Diagnostics.Contracts
{
  [AttributeUsage(global::System.AttributeTargets.Assembly)]
  internal sealed class ContractDeclarativeAssemblyAttribute : global::System.Attribute
  {
  }
}
