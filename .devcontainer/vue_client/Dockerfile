ARG VARIANT="16-bullseye"
FROM mcr.microsoft.com/vscode/devcontainers/typescript-node:0-${VARIANT}

RUN useradd -ms /bin/bash vscode
RUN bash -c "source /usr/local/share/nvm/nvm.sh && npm install -g pnpm @vue/cli" 2>&1
