FROM mcr.microsoft.com/devcontainers/dotnet:1-9.0
# Instalar dependencias para QuestPDF (SkiaSharp requiere libfontconfig y fuentes)
RUN apt-get update && apt-get install -y --no-install-recommends \
    libfontconfig1 \
    fontconfig \
    fonts-dejavu \
    && rm -rf /var/lib/apt/lists/*

# Instalar herramientas de EF Core globalmente
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"