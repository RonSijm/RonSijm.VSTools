cd..
cd src
dotnet pack

cd..
cd nupkg
dotnet tool uninstall --global RonSijm.VSTools.CLI
dotnet tool install --global --add-source ./../nupkg RonSijm.VSTools.CLI