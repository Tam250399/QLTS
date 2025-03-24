const formatter = new Intl.NumberFormat("vi-VN", {
  style: "decimal",
  minimumFractionDigits: 0,
});

const formatCurrencyVND = (
  value: number | string,
  unit: string = "VNĐ"
): string => {
  try {
    let numberValue: number;
    if (typeof value === "string") {
      const cleanValue = value.replace(/[^0-9]/g, "");
      numberValue = parseInt(cleanValue, 10);
    } else {
      numberValue = value;
    }

    // Kiểm tra nếu giá trị không hợp lệ
    if (isNaN(numberValue)) {
      return ` ${unit}`;
    }

    // Định dạng số với dấu phân cách hàng nghìn
    const formattedNumber = formatter.format(numberValue);

    // Trả về chuỗi đã định dạng với đơn vị
    return `${formattedNumber} ${unit}`;
  } catch (error) {
    console.error("Error formatting currency:", error);
    return `0 ${unit}`; // Giá trị mặc định nếu có lỗi
  }
};

export default formatCurrencyVND;
