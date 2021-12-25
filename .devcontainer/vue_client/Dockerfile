# See here for image contents: https://github.com/microsoft/vscode-dev-containers/tree/v0.209.6/containers/typescript-node/.devcontainer/base.Dockerfile

# [Choice] Node.js version (use -bullseye variants on local arm64/Apple Silicon): 16, 14, 12, 16-bullseye, 14-bullseye, 12-bullseye, 16-buster, 14-buster, 12-buster
ARG VARIANT="16-bullseye"
FROM mcr.microsoft.com/vscode/devcontainers/typescript-node:0-${VARIANT}

RUN useradd -ms /bin/bash vscode
# [Optional] Uncomment this line to install global node packages.
RUN bash -c "source /usr/local/share/nvm/nvm.sh && npm install -g pnpm @vue/cli" 2>&1
