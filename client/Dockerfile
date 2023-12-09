# Node.js tabanlı bir imaj kullan
FROM node:latest

# Çalışma dizinini /app olarak belirle
WORKDIR /src

# .env dosyasını kopyala
COPY .env ./

# Gerekli dosyaları kopyala
COPY package.json package-lock.json ./

# Bağımlılıkları yükle
RUN npm install

# Diğer dosyaları kopyala
COPY . ./

# Uygulamayı başlat
CMD ["npm", "start"]
