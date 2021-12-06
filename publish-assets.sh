assets=()
for asset in *.nupkg 
do 
assets+=("$asset")
done
for asset in *.snupkg 
do 
assets+=("$asset")
done
gh release upload ${{ github.event.release.tag_name }} "${assets[@]}"