using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using System.Linq;
using UnityEditor.PackageManager.Requests;

[InitializeOnLoad]
public static class PackageVersionChecker {
  // We could also crossref with this package's version
  static readonly string expectedVersion = "3.3.0"; 
  static readonly string dependencyPackage = "org.mujoco";
  static ListRequest Request;

  static PackageVersionChecker() {
    // Run the check once on startup
    CheckCurrentPackages();

    // Also run the check whenever packages are registered (installed/updated)
    Events.registeredPackages += OnPackagesRegistered;
  }

  static void CheckCurrentPackages() {
    Request = Client.List(); // List packages installed for the Project
    EditorApplication.update += Progress;
  }

  static void Progress() {
    if (Request.IsCompleted) {
      if (Request.Status == StatusCode.Success)
        CheckInPackages(Request.Result);
      else if (Request.Status >= StatusCode.Failure)
        Debug.Log(Request.Error.message);

      EditorApplication.update -= Progress;
    }
  }

  private static void CheckInPackages(IEnumerable<UnityEditor.PackageManager.PackageInfo> packages) {
    string dependencyVersion = null;
    foreach (var package in packages) {
      if (package.name == dependencyPackage)
        dependencyVersion = package.version;
    }

    if (dependencyVersion == null) {
      Debug.LogError($"MuJoCo package {dependencyVersion} is missing! "+
                     $"Required version: {expectedVersion}");
    } else if (dependencyVersion != expectedVersion) {
      Debug.LogError($"MuJoCo version mismatch! "+
                     $"Expected: {expectedVersion}, Found: {dependencyVersion}. "+
                     $"Please install matching versions of the binaries and bindings.");
    }
  }

  private static void OnPackagesRegistered(PackageRegistrationEventArgs args) {

    CheckInPackages(args.added.Concat(args.changedTo));

  }
}