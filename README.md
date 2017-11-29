# OnTheFlyCompilation
Purpose of the project is to check, is there any way to achive On the fly compilation in UWP app and try to access dynamic codes.

### Comparison
|         | UWP           | Win32/WPF  |
| ------------- |-------------| -----|
| User Rights      | Runs applications in an AppContainer security context | run as "Standard User" or as "Administrator" |
| Deployment Options      | The AppX files are deployed only by the system by Microsoft store or by signed AppX deployment file | User can select the location and drive. And the packages can be in the form of MSI, EXE, ZIP or any other supported format |
| File Access      | Have full access to App Install folder and limited access to the system and user data. Can add some more features using Capabilities request. For file access need to provide file extension and NO support for restricted files (eg. DLL, EXE) | User has full file access based of the user type |
| Dynamic Compilation      | No access to dynamic compilation libraries. | Can do dynamic compilation |

### Reference
https://docs.microsoft.com/en-us/dotnet/framework/reflection-and-codedom/dynamic-source-code-generation-and-compilation

https://stackify.com/what-is-c-reflection/

https://stackoverflow.com/questions/36286806/uwp-limitations-in-desktop-apps
