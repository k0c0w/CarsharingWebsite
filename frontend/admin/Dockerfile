FROM node:latest
WORKDIR /admin_dashboard
COPY package.json ./
RUN npm install
COPY . .
CMD ["npm", "start"]
