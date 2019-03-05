FROM node:10.15.1
WORKDIR /app
EXPOSE 4200

COPY "OpenCdn.Client/package.json" ./
COPY "OpenCdn.Client/yarn.lock" ./

RUN npm run restore
COPY "OpenCdn.Client/*" ./

RUN npm run start:docker
