write-host "Packing all projects"  -foreground 'green'
dotnet pack > ".\packed\packingOutput.txt"
If (-Not (Test-Path -Path packed)){
	mkdir packed
}
$arr = Get-ChildItem . -recurse *.nupkg |
       Foreach-Object {$_.FullName}
ForEach ($file in $arr)
	{
		if( -Not($file -clike ".\packed\*")){
			$fileName=[System.IO.Path]::GetFileName($file)
			$packPath=".\packed\$fileName" 
			write-host "Moving $fileName"  -foreground 'yellow'
			Move-Item -Path $file -Destination $packPath -Force
		}
	}
