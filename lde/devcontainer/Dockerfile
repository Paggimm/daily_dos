FROM mcr.microsoft.com/devcontainers/dotnet:0-7.0

# [Choice] Node.js version: none, lts/*, 16, 14, 12, 10
ARG NODE_VERSION
RUN test -n "$NODE_VERSION"
RUN if [ "${NODE_VERSION}" != "none" ]; then su vscode -c "umask 0002 && . /usr/local/share/nvm/nvm.sh && nvm install ${NODE_VERSION} 2>&1"; fi

# [Optional] Uncomment this section to install additional OS packages.
# RUN apt-get update && export DEBIAN_FRONTEND=noninteractive \
#     && apt-get -y install --no-install-recommends <your-package-list-here>

# [Optional] Uncomment this line to install global node packages.
# RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g @vue/cli" 2>&1
