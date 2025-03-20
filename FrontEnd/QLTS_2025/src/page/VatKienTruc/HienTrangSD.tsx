import React from "react";
import {
  Typography,
  FormGroup,
  FormControlLabel,
  Checkbox,
  Box,
} from "@mui/material";
import { Controller } from "react-hook-form";

const HienTrangSD = ({ control }) => {
  return (
    <Box
      sx={{
        border: "1px solid #007bff",
        borderRadius: 2,
        p: 3,
        bgcolor: "white",
        boxShadow: 2,
        position: "relative",
      }}
    >
      <Typography
        variant="h6"
        sx={{
          position: "absolute",
          top: "-12px",
          left: "15px",
          backgroundColor: "white",
          padding: "0 8px",
          color: "#007bff",
          fontSize: "14px", // Giảm kích thước tiêu đề
          fontWeight: "bold",
        }}
      >
        Hiện trạng sử dụng
      </Typography>
      <FormGroup
        row
        sx={{
          display: "flex",
          justifyContent: "space-between",
          flexWrap: "nowrap",
          gap: 2,
        }}
      >
        <FormControlLabel
          control={
            <Controller
              name="stateManagement"
              control={control}
              disabled
              render={({ field }) => (
                <Checkbox
                  {...field}
                  checked={field.value}
                  onChange={(e) => field.onChange(e.target.checked)}
                />
              )}
            />
          }
          label="Quan lý nhà nước"
          sx={{ marginRight: 0 }} // Removes default margin for better spacing control
        />
        <FormControlLabel
          control={
            <Controller
              name="businessNotOperating"
              control={control}
              render={({ field }) => (
                <Checkbox
                  {...field}
                  checked={field.value}
                  onChange={(e) => field.onChange(e.target.checked)}
                />
              )}
            />
          }
          label="HDSN-Không KD"
          sx={{ marginRight: 0 }}
        />
        <FormControlLabel
          control={
            <Controller
              name="businessOperating"
              control={control}
              render={({ field }) => (
                <Checkbox
                  {...field}
                  checked={field.value}
                  onChange={(e) => field.onChange(e.target.checked)}
                />
              )}
            />
          }
          label="HDSN-Kinh doanh"
          sx={{ marginRight: 0 }}
        />
        <FormControlLabel
          control={
            <Controller
              name="businessJointVenture"
              control={control}
              render={({ field }) => (
                <Checkbox
                  {...field}
                  checked={field.value}
                  onChange={(e) => field.onChange(e.target.checked)}
                />
              )}
            />
          }
          label="HDSN-LDLK"
          sx={{ marginRight: 0 }}
        />
        <FormControlLabel
          control={
            <Controller
              name="businessLeased"
              control={control}
              render={({ field }) => (
                <Checkbox
                  {...field}
                  checked={field.value}
                  onChange={(e) => field.onChange(e.target.checked)}
                />
              )}
            />
          }
          label="HDSN-Cho thuê"
          sx={{ marginRight: 0 }}
        />
        <FormControlLabel
          control={
            <Controller
              name="otherUse"
              control={control}
              render={({ field }) => (
                <Checkbox
                  {...field}
                  checked={field.value}
                  onChange={(e) => field.onChange(e.target.checked)}
                />
              )}
            />
          }
          label="Sử dụng khác"
          sx={{ marginRight: 0 }}
        />
      </FormGroup>
    </Box>
  );
};

export default HienTrangSD;
