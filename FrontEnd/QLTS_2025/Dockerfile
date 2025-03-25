# Sử dụng Node.js image chính thức làm base image (giai đoạn build)
FROM node:20-alpine

# Thiết lập thư mục làm việc trong container
WORKDIR /app

# Sao chép file package.json và package-lock.json để cài đặt dependencies
COPY package*.json ./

# Cài đặt dependencies
RUN npm install

# Sao chép toàn bộ mã nguồn vào container
COPY . .

# Expose port 80 để truy cập ứng dụng
EXPOSE 5173

# Khởi động Nginx
CMD ["npm", "run", "dev"]