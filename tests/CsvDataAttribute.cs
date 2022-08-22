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
    List<object[]> allCases = new List<object[]>();
    var paramTypes = testMethod.GetParameters().Select(x => x.ParameterType).ToArray();
    for (var i = 0; i < lines.Length; i++)
    {
      var line = lines[i];
      string[] columns = line.Split(';');
      if (columns.Length != paramTypes.Length)
      {
        throw new ArgumentException(
          $"The number of columns at line {i} does not match the amount of params, {paramTypes.Length}");
      }

      List<object> testParams = new List<object>();
      for (int j = 0; j < paramTypes.Length; j++)
      {
        var type = paramTypes[j];
        switch (Type.GetTypeCode(type))
        {
          case TypeCode.UInt32:
            testParams.Add(UInt32.Parse(columns[j]));
            break;
          case TypeCode.Int32:
            testParams.Add(Int32.Parse(columns[j]));
            break;
          case TypeCode.Single:
            testParams.Add(Single.Parse(columns[j]));
            break;
          default:
            testParams.Add(columns[j]);
            break;
        }
      }

      allCases.Add(testParams.ToArray());
    }

    return allCases;
  }
}