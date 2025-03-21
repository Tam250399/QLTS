import React from "react";
import "../App.css";
import {
  FaGlobe,
  FaHome,
  FaChartLine,
  FaCar,
  FaShip,
  FaDesktop,
  FaTree,
  FaCube,
  FaCircleNotch,
  FaSlidersH,
} from "react-icons/fa";
import { useNavigate } from "react-router-dom";

const Trangchu = () => {
  const cards = [
    { icon: <FaGlobe />, text: "Đất", color: "#00C4B4" },
    { icon: <FaHome />, text: "Nhà", color: "#1E90FF" },
    { icon: <FaChartLine />, text: "Vật kiến trúc", color: "#1E90FF" },
    { icon: <FaCar />, text: "Ô tô", color: "#FF4500" },
    { icon: <FaShip />, text: "PTVT khác", color: "#1E90FF" },
    { icon: <FaDesktop />, text: "Máy móc thiết bị", color: "#FFD700" },
    { icon: <FaTree />, text: "Cây lâu năm, sylv", color: "#32CD32" },
    { icon: <FaCube />, text: "TSCĐ hữu hình khác", color: "#808080" },
    { icon: <FaCircleNotch />, text: "TSCĐ vô hình", color: "#FF4500" },
    {
      icon: <FaSlidersH />,
      text: "Tài sản quản lý như TSCĐ",
      color: "#1E90FF",
    },
  ];

  const navigate = useNavigate();

  const handleCardClick = (title) => {
    switch (title) {
      case "Đất":
        navigate("/home");
        break;
      case "Nhà":
        navigate("/nha");
        break;
      case "Vật kiến trúc":
        navigate("/taisanvkt");
        break;
      default:
        navigate("/layout");
        break;
    }
  };

  return (
    <div className="container">
      <h2 className="title pb-10">Nhập số dư đầu kỳ - Mới chọn nhóm tài sản</h2>
      <div className="grid">
        {cards.map((card, index) => (
          <div
            key={index}
            className="card "
            onClick={() => handleCardClick(card.text)}
          >
            <div className="icon" style={{ color: card.color }}>
              {card.icon}
            </div>
            <p>{card.text}</p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Trangchu;
