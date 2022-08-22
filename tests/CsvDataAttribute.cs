using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit.Sdk;

namespace PSOMapRNGSimulation.Tests;

public class CsvDataAttribute : DataAttribute
{
  private readonly string _filepath;

  public CsvDataAttribute(string filepath)
  {
    _filepath = filepath;
  }

  public override IEnumerable<object[]> GetData(MethodInfo testMethod)
  {
    if (!File.Exists(_filepath))
      throw new ArgumentException($"The file {_filepath} does not exist");

    string[] lines = File.ReadAllLines(_filepath);
    var paramTypes = testMethod.GetParameters().Select(x => x.ParameterType).ToArray();
    for(int i = 0; i < lines.Length; i++)
    {
      string[] columns = lines[i].Split(';');
      List<object> testParams = new List<object>();
      foreach (var type in paramTypes)
      {
        switch (Type.GetTypeCode(type))
        {
          case TypeCode.UInt32:
            testParams.Add(UInt32.Parse(columns[i]));
            break;
          case TypeCode.Int32:
            testParams.Add(Int32.Parse(columns[i]));
            break;
          case TypeCode.Single:
            testParams.Add(Single.Parse(columns[i]));
            break;
          default:
            testParams.Add(columns[i]);
            break;
        }
      }
      yield return new object[] { testParams };
    }
  }
}