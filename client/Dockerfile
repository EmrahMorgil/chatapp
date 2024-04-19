FROM node:14 

WORKDIR /app

COPY . .

ENV REACT_APP_API_URI=https://chatapp-api.emrahmorgil.com

RUN npm install

CMD ["npm", "start"]
