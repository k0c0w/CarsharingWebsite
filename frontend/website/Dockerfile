FROM node:latest
WORKDIR /website
COPY package.json ./
RUN npm install
ARG REACT_APP_MY_VARIABLE
ENV REACT_APP_MY_VARIABLE $REACT_APP_MY_VARIABLE
COPY . .
CMD ["npm", "start"]
