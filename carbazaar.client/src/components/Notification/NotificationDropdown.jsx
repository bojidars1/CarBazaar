import React, { useEffect, useState } from "react";
import api from "../../api/api";
import { Menu, MenuItem, Badge, IconButton, Link } from "@mui/material";
import NotificationsIcon from "@mui/icons-material/Notifications";

const NotificationDropdown = () => {
  const [notifications, setNotifications] = useState([]);
  const [unreadCount, setUnreadCount] = useState(0);
  const [anchorEl, setAnchorEl] = useState(null);

  const fetchNotifications = async () => {
    try {
      const response = await api.get("/Notification/get-notifications", {
        params: { page: 1, pageSize: 10 },
      });
      const unreadNotifications = response.data.items.filter((n) => !n.isRead);
      setNotifications(unreadNotifications);
      setUnreadCount(unreadNotifications.length);
    } catch (err) {
      console.error("Failed to fetch notifications:", err);
    }
  };

  const handleMarkAsRead = async () => {
    try {
      const unreadIds = notifications.map((n) => n.id);
      await api.post("/Notification/mark-as-read", unreadIds);
      setUnreadCount(0);
      fetchNotifications();
    } catch (err) {
      console.error("Failed to mark notifications as read:", err);
    }
  };

  useEffect(() => {
    fetchNotifications();
  }, []);

  return (
    <>
      <Badge badgeContent={unreadCount} color="error">
        <IconButton
          color="inherit"
          onClick={(e) => setAnchorEl(e.currentTarget)}
        >
          <NotificationsIcon />
        </IconButton>
      </Badge>
      <Menu
        anchorEl={anchorEl}
        open={Boolean(anchorEl)}
        onClose={() => setAnchorEl(null)}
      >
        {notifications.slice(0, 1).map((notification) => (
          <MenuItem key={notification.id}>
            {notification.message}
          </MenuItem>
        ))}
        {notifications.length > 1 && (
          <MenuItem>
            <Link href="/notifications">+{notifications.length - 1} more</Link>
          </MenuItem>
        )}
        {notifications.length > 0 && (
          <MenuItem onClick={handleMarkAsRead}>Mark All as Read</MenuItem>
        )}
        {notifications.length === 0 && <MenuItem>No Notifications</MenuItem>}
      </Menu>
    </>
  );
};

export default NotificationDropdown;