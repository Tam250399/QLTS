import {
  Button,
  Dialog,
  DialogContent,
  DialogTitle,
  IconButton,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TablePagination,
  TableRow,
  TextField,
  Typography,
} from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";
import { useEffect, useState } from "react";
import { GetListChonDats } from "../../service/ServiceNha";
import { KhuonVienDat } from "../../validateform/thongtinnha";

interface ChonDatProps {
  open: boolean;
  handleClose: () => void;
  handleChonKhuonVienDat: (id: number, address: string) => void;
}

const ChonDat: React.FC<ChonDatProps> = ({
  open,
  handleClose,
  handleChonKhuonVienDat,
}) => {
  useEffect(() => {
    const fetchData = async () => {
      try {
        const data = {
          KeySearch: "",
          donViId: 3,
          pageIndex: 1,
          pageSize: 100,
        };
        const response = await GetListChonDats(data);
        setFilteredData(response);
      } catch (error) {
        console.error("Lỗi khi tải dữ liệu:", error);
      }
    };

    fetchData();
  }, []);
  // const data = [
  //   {
  //     id: 1,
  //     address: "Văn phòng Chủ tịch nước, Phường Ngọc Hà, Ba Đình, Hà Nội",
  //     type: "Đất trụ sở",
  //     status: "Đã duyệt",
  //   },
  //   {
  //     id: 2,
  //     address: "Văn phòng Chủ tịch nước, Số 1 Hoàng Hoa Thám, Ba Đình, Hà Nội",
  //     type: "Đất trụ sở",
  //     status: "Đã duyệt",
  //   },
  //   {
  //     id: 3,
  //     address:
  //       "Văn phòng Chủ tịch nước, Số 1 ngõ 123A phố Thụy Khuê, Tây Hồ, Hà Nội",
  //     type: "Đất trụ sở",
  //     status: "Đã duyệt",
  //   },
  //   {
  //     id: 4,
  //     address:
  //       "Nhà khách Chính phủ - Bộ Ngoại giao, Tràng Tiền, Hoàn Kiếm, Hà Nội",
  //     type: "Đất hoạt động sự nghiệp",
  //     status: "Chờ duyệt",
  //   },
  //   {
  //     id: 5,
  //     address:
  //       "Nhà khách Chính phủ - Bộ Ngoại giao, Tràng Tiền, Hoàn Kiếm, Hà Nội",
  //     type: "Đất hoạt động sự nghiệp",
  //     status: "Chờ duyệt",
  //   },
  //   {
  //     id: 6,
  //     address:
  //       "Nhà khách Chính phủ - Bộ Ngoại giao, Tràng Tiền, Hoàn Kiếm, Hà Nội",
  //     type: "Đất hoạt động sự nghiệp",
  //     status: "Chờ duyệt",
  //   },
  //   {
  //     id: 7,
  //     address:
  //       "Nhà khách Chính phủ - Bộ Ngoại giao, Tràng Tiền, Hoàn Kiếm, Hà Nội",
  //     type: "Đất hoạt động sự nghiệp",
  //     status: "Chờ duyệt",
  //   },
  // ];
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);

  const [searchText, setSearchText] = useState("");
  const [filteredData, setFilteredData] = useState<KhuonVienDat[]>([]);
  const handleSearch = () => {
    const results = filteredData.filter((item) =>
      item.DIA_CHI.toLowerCase().includes(searchText.toLowerCase())
    );
    setFilteredData(results);
    setPage(0);
  };

  const handleChangePage = (_: any, newPage: any) => setPage(newPage);
  const handleChangeRowsPerPage = (event: any) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  return (
    <Dialog open={open} onClose={handleClose} maxWidth="md" fullWidth>
      <DialogTitle
        sx={{
          display: "flex",
          justifyContent: "space-between",
          borderBottom: "1px solid #ccc",
          alignItems: "center",
          margin: "0 0 30px 10px",
          fontSize: "30px",
        }}
      >
        Chọn khuôn viên đất
        <IconButton onClick={handleClose} size="small">
          <CloseIcon />
        </IconButton>
      </DialogTitle>
      <DialogContent>
        <div
          style={{
            display: "flex",
            alignItems: "center",
            marginBottom: 30,
            justifyContent: "center",
          }}
        >
          <span style={{ marginRight: "20px" }}>Từ khóa</span>
          <TextField
            sx={{
              minWidth: "400px",
              "& .MuiInputBase-root": {
                height: "30px", // Điều chỉnh chiều cao tổng thể
              },
              "& .MuiInputBase-input": {
                padding: "5px 10px", // Điều chỉnh khoảng cách nội dung bên trong
                fontSize: "14px", // Giữ chữ không bị quá lớn
              },
            }}
            size="small"
            variant="outlined"
            value={searchText}
            onChange={(e) => setSearchText(e.target.value)}
          />

          <Button
            variant="contained"
            sx={{
              marginLeft: "20px",
              height: "30px",
              fontWeight: "200",
              textTransform: "none",
            }}
            onClick={handleSearch}
          >
            Tìm kiếm
          </Button>
        </div>
        <TableContainer>
          <Table>
            <TableHead>
              <TableRow
                sx={{
                  backgroundColor: "#1976d2",
                  color: "white",
                  textAlign: "center",
                }}
              >
                <TableCell
                  sx={{
                    color: "white",
                    fontWeight: "bold",
                    textAlign: "center",
                  }}
                >
                  STT
                </TableCell>
                <TableCell
                  sx={{
                    color: "white",
                    fontWeight: "bold",
                    textAlign: "center",
                  }}
                >
                  Địa chỉ đất
                </TableCell>
                <TableCell
                  sx={{
                    color: "white",
                    fontWeight: "bold",
                    textAlign: "center",
                  }}
                >
                  Loại tài sản
                </TableCell>
                <TableCell
                  sx={{
                    color: "white",
                    fontWeight: "bold",
                    textAlign: "center",
                  }}
                >
                  Trạng thái
                </TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {filteredData
                .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                .map((row, index) => (
                  <TableRow key={row.ID}>
                    <TableCell>{index + 1 + page * rowsPerPage}</TableCell>
                    <TableCell>
                      <Typography
                        sx={{
                          cursor: "pointer",
                          color: "black",
                          "&:hover": {
                            color: "blue",
                          },
                        }}
                        onClick={() =>
                          handleChonKhuonVienDat(row.ID, row.DIA_CHI)
                        }
                      >
                        {row.DIA_CHI}
                      </Typography>
                    </TableCell>
                    <TableCell>{row.LOAI_TAI_SAN}</TableCell>
                    <TableCell>{row.TRANG_THAI}</TableCell>
                  </TableRow>
                ))}
            </TableBody>
          </Table>
        </TableContainer>
        <TablePagination
          rowsPerPageOptions={[5, 10, 15]}
          component="div"
          count={filteredData.length}
          rowsPerPage={rowsPerPage}
          page={page}
          onPageChange={handleChangePage}
          onRowsPerPageChange={handleChangeRowsPerPage}
        />
      </DialogContent>
    </Dialog>
  );
};
export default ChonDat;
