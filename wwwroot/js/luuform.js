// Thu thập dữ liệu từ form 1 và form 2
let addressFormData = new FormData(document.getElementById('formtimkiem'));
let ratingFormData = new FormData(document.getElementById('formsao'));

// Lấy giá trị từ dữ liệu form
let address = addressFormData.get('address');
let rating = ratingFormData.get('rating');

// Kết hợp dữ liệu vào một cấu trúc dữ liệu
let combinedData = {
    address: address,
    rating: rating
};

// Xử lý dữ liệu hoặc hiển thị kết quả
console.log('', combinedData);
// Tiếp tục xử lý hoặc hiển thị kết quả ở đây
