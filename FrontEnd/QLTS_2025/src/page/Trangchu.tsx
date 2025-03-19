import { BrowserRouter as Router, Link } from "react-router-dom";
import { Grid, Card, CardContent, Typography, Box } from "@mui/material";
import PublicIcon from "@mui/icons-material/Public";
import HomeIcon from "@mui/icons-material/Home";
import AnalyticsIcon from "@mui/icons-material/Analytics";
import DirectionsCarIcon from "@mui/icons-material/DirectionsCar";
import DirectionsBoatIcon from "@mui/icons-material/DirectionsBoat";
import ComputerIcon from "@mui/icons-material/Computer";
import ParkIcon from "@mui/icons-material/Park";
// import CubeIcon from '@mui/icons-material/Cube'; // Không có icon Cube trực tiếp, bạn có thể dùng icon khác hoặc tự tạo
import LinkIcon from "@mui/icons-material/Link";
import SettingsIcon from "@mui/icons-material/Settings";

const menuItems = [
  { title: "Đất", icon: <PublicIcon />, link: "/dat" },
  { title: "Nhà", icon: <HomeIcon />, link: "/nha" },
  { title: "Vật kiến trúc", icon: <AnalyticsIcon />, link: "/vat-kien-truc" },
  { title: "Ô tô", icon: <DirectionsCarIcon />, link: "/o-to" },
  { title: "PTVT khác", icon: <DirectionsBoatIcon />, link: "/ptvt-khac" },
  {
    title: "Máy móc thiết bị",
    icon: <ComputerIcon />,
    link: "/may-moc-thiet-bi",
  },
  { title: "Cây lâu năm, sylv", icon: <ParkIcon />, link: "/cay-lau-nam" },
  //   { title: 'TSCĐ hữu hình khác', icon: <CubeIcon />, link: '/tscd-huu-hinh-khac' },
  { title: "TSCĐ vô hình", icon: <LinkIcon />, link: "/tscd-vo-hinh" },
  {
    title: "Tài sản quản lý như TSCĐ",
    icon: <SettingsIcon />,
    link: "/tai-san-quan-ly",
  },
];

const Menu = () => {
  return (
    <Box sx={{ padding: 2 }}>
      <Grid container spacing={2}>
        {menuItems.map((item, index) => (
          <Grid item xs={12} sm={6} md={3} key={index}>
            <Link to={item.link} style={{ textDecoration: "none" }}>
              <Card
                sx={{
                  display: "flex",
                  alignItems: "center",
                  padding: 2,
                  backgroundColor: index === 6 ? "#e3f2fd" : "#fff", // Highlight "Cây lâu năm, sylv"
                  border: index === 6 ? "1px solid #2196f3" : "1px solid #ddd",
                  "&:hover": {
                    backgroundColor: "#f5f5f5",
                  },
                }}
              >
                <Box
                  sx={{
                    marginRight: 2,
                    color: index === 8 ? "#f44336" : "#1976d2",
                  }}
                >
                  {item.icon}
                </Box>
                <CardContent sx={{ padding: 0 }}>
                  <Typography
                    variant="body1"
                    sx={{ color: index === 8 ? "#f44336" : "#000" }}
                  >
                    {item.title}
                  </Typography>
                </CardContent>
              </Card>
            </Link>
          </Grid>
        ))}
      </Grid>
    </Box>
  );
};

const Trangchu = () => {
  return (
    <Router>
      <Menu />
    </Router>
  );
};

export default Trangchu;
