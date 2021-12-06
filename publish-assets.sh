assets=()
for asset in *.nupkg 
do 
assets+=("$asset")
done
for asset in *.snupkg 
do 
assets+=("$asset")
done
gh release upload $1 "${assets[@]}"