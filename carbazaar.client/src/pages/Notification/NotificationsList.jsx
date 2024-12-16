import React, { useEffect, useState } from "react";
import api from "../../api/api";
import { Box, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Button, Typography, Pagination } from "@mui/material";
import { Link } from "react-router-dom";

const NotificationsList = () => {
  const [notifications, setNotifications] = useState([]);
  const [totalPages, setTotalPages] = useState(1);
  const [page, setPage] = useState(1);
  const [loading, setLoading] = useState(true);

  const pageSize = 10;

  const fetchNotifications = async (page) => {
    try {
      const response = await api.get("/Notification/get-notifications", {
        params: { page, pageSize },
      });
      setNotifications(response.data.items);
      setTotalPages(response.data.totalPages);
      setLoading(false);
    } catch (err) {
      console.error("Failed to fetch notifications:", err);
      setLoading(false);
    }
  };

  const handlePageChange = (event, value) => {
    setPage(value);
  };

  const handleMarkAsDeleted = async (id) => {
    try {
      await api.post("/Notification/mark-as-deleted", [id]);
      fetchNotifications(page);
    } catch (err) {
      console.error("Failed to mark notification as deleted:", err);
    }
  };

  const handleMarkAsRead = async (notificationId) => {
    try {
      await api.post("/Notification/mark-as-read", [notificationId]);
    } catch (err) {
      console.error("Failed to mark notification as read:", err);
    }
  };

  useEffect(() => {
    fetchNotifications(page);
  }, [page]);

  return (
    <Box sx={{ p: 2 }}>
      <Typography variant="h4" sx={{ textAlign: "center", mb: 3 }}>
        Notifications
      </Typography>

      {loading ? (
        <Typography variant="body1" sx={{ textAlign: "center" }}>
          Loading...
        </Typography>
      ) : (
        <TableContainer component={Paper}>
          <Table sx={{ minWidth: 650 }} aria-label="notifications table">
            <TableHead>
              <TableRow>
                <TableCell>Message</TableCell>
                <TableCell align="right">Actions</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {notifications.map((notification) => (
                <TableRow
                  key={notification.id}
                  sx={{
                    backgroundColor: notification.isRead ? "#f5f5f5" : "white",
                    "&:hover": {
                      backgroundColor: notification.isRead ? "#f0f0f0" : "#e0e0e0",
                    },
                  }}
                >
                  <TableCell>{notification.message}</TableCell>
                  <TableCell align="right">
                    <Box sx={{ display: "flex", gap: "0.5em", justifyContent: "flex-end" }}>
                      {!notification.isRead && (
                        <Button
                          variant="contained"
                          color="primary"
                          size="small"
                          component={Link}
                          to={`/chat/${notification.carListingId}/${notification.senderId}`}
                          onClick={() => handleMarkAsRead(notification.id)}
                        >
                          View
                        </Button>
                      )}
                      <Button
                        variant="contained"
                        color="error"
                        size="small"
                        onClick={() => handleMarkAsDeleted(notification.id)}
                      >
                        Delete
                      </Button>
                    </Box>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      )}

      <Box sx={{ display: "flex", justifyContent: "center", mt: 3 }}>
        <Pagination
          count={totalPages}
          page={page}
          onChange={handlePageChange}
          color="primary"
          size="large"
        />
      </Box>
    </Box>
  );
};

export default NotificationsList;