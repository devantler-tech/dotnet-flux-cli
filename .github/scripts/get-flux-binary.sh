#!/bin/bash
set -e

get() {
  local url=$1
  local binary=$2
  local target_dir=$3
  local target_name=$4
  local isTar=$5

  # check if tar
  if [ "$isTar" = true ]; then
    curl -LJ "$url" | tar xvz -C "$target_dir" "$binary"
    mv "$target_dir/$binary" "${target_dir}/$target_name"
  elif [ "$isTar" = false ]; then
    curl -LJ "$url" -o "$target_dir/$target_name"
  fi
  chmod +x "$target_dir/$target_name"
}

get "https://getbin.io/fluxcd/flux2?os=darwin&arch=amd64" "flux" "src/Devantler.FluxCLI/assets/binaries" "flux-darwin-amd64" true
get "https://getbin.io/fluxcd/flux2?os=darwin&arch=arm64" "flux" "src/Devantler.FluxCLI/assets/binaries" "flux-darwin-arm64" true
get "https://getbin.io/fluxcd/flux2?os=linux&arch=amd64" "flux" "src/Devantler.FluxCLI/assets/binaries" "flux-linux-amd64" true
get "https://getbin.io/fluxcd/flux2?os=linux&arch=arm64" "flux" "src/Devantler.FluxCLI/assets/binaries" "flux-linux-arm64" true
