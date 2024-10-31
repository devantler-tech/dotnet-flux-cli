#!/bin/bash
set -e

get() {
  local url=$1
  local binary=$2
  local target_dir=$3
  local target_name=$4
  local archiveType=$5

  echo "Downloading $target_name from $url"
  if [ "$archiveType" = "tar" ]; then
    curl -LJ "$url" | tar xvz -C "$target_dir" "$binary"
    mv "$target_dir/$binary" "${target_dir}/$target_name"
  elif [ "$archiveType" = "zip" ]; then
    curl -LJ "$url" -o "$target_dir/$target_name.zip"
    unzip -o "$target_dir/$target_name.zip" -d "$target_dir"
    mv "$target_dir/$binary" "${target_dir}/$target_name"
    rm "$target_dir/$target_name.zip"
  elif [ "$archiveType" = false ]; then
    curl -LJ "$url" -o "$target_dir/$target_name"
  fi
  chmod +x "$target_dir/$target_name"
}

get "https://getbin.io/fluxcd/flux2?os=darwin&arch=amd64" "flux" "Devantler.FluxCLI/runtimes/osx-x64/native" "flux-osx-x64" "tar"
get "https://getbin.io/fluxcd/flux2?os=darwin&arch=arm64" "flux" "Devantler.FluxCLI/runtimes/osx-arm64/native" "flux-osx-arm64" "tar"
get "https://getbin.io/fluxcd/flux2?os=linux&arch=amd64" "flux" "Devantler.FluxCLI/runtimes/linux-x64/native" "flux-linux-x64" "tar"
get "https://getbin.io/fluxcd/flux2?os=linux&arch=arm64" "flux" "Devantler.FluxCLI/runtimes/linux-arm64/native" "flux-linux-arm64" "tar"
get "https://getbin.io/fluxcd/flux2?os=windows&arch=amd64" "flux.exe" "Devantler.FluxCLI/runtimes/win-x64/native" "flux-win-x64.exe" "zip"
get "https://getbin.io/fluxcd/flux2?os=windows&arch=arm64" "flux.exe" "Devantler.FluxCLI/runtimes/win-arm64/native" "flux-win-arm64.exe" "zip"
