import React from "react";
import { Container, Box, Typography, TextField, Button, Alert } from "@mui/material";
import { Formik, Form, Field } from "formik";
import * as Yup from "yup";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { setUser } from "../redux/userSlice";
import { setAuthenticated } from "../redux/authSlice";
import { jwtDecode } from "jwt-decode";
import api from "../api/api";

const RegisterSchema = Yup.object().shape({
  email: Yup.string()
    .email("Invalid email address")
    .required("Email is required"),
  password: Yup.string()
    .min(6, "Password must be at least 6 characters")
    .required("Password is required"),
  confirmPassword: Yup.string()
    .oneOf([Yup.ref("password")], "Passwords must match")
    .required("Confirm Password is required"),
});

const Register = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const handleRegister = async (values, { setSubmitting, setErrors }) => {
    try {
      const response = await api.post("/account/register", {
        email: values.email,
        password: values.password,
      });

      const { accessToken } = response.data;

      const decodedToken = jwtDecode(accessToken);
      const user = {
        userId: decodedToken.sub,
        userEmail: decodedToken.email,
        carListings: decodedToken.CarListings,
      };

      localStorage.setItem("token", accessToken);
      dispatch(setAuthenticated(true));
      dispatch(setUser(user));

      navigate("/");
    } catch (err) {
      console.error(err);
      if (err.response && err.response.data) {
        setErrors({ general: err.response.data.message || "Registration failed" });
      } else {
        setErrors({ general: "Something went wrong. Please try again later." });
      }
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <Container maxWidth="xs">
      <Box
        sx={{
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
          marginTop: 8,
          padding: 3,
          borderRadius: 2,
          boxShadow: 2,
          bgcolor: "background.paper",
        }}
      >
        <Typography variant="h4" sx={{ mb: 4 }}>
          Register
        </Typography>

        <Formik
          initialValues={{ email: "", password: "", confirmPassword: "" }}
          validationSchema={RegisterSchema}
          onSubmit={handleRegister}
        >
          {({ errors, touched, isSubmitting }) => (
            <Form style={{ width: "100%" }}>
              {errors.general && (
                <Alert severity="error" sx={{ mb: 2 }}>
                  {errors.general}
                </Alert>
              )}

              <Field
                as={TextField}
                label="Email"
                name="email"
                type="email"
                fullWidth
                required
                error={touched.email && Boolean(errors.email)}
                helperText={touched.email && errors.email}
                sx={{ mb: 2 }}
              />

              <Field
                as={TextField}
                label="Password"
                name="password"
                type="password"
                fullWidth
                required
                error={touched.password && Boolean(errors.password)}
                helperText={touched.password && errors.password}
                sx={{ mb: 2 }}
              />

              <Field
                as={TextField}
                label="Confirm Password"
                name="confirmPassword"
                type="password"
                fullWidth
                required
                error={touched.confirmPassword && Boolean(errors.confirmPassword)}
                helperText={touched.confirmPassword && errors.confirmPassword}
                sx={{ mb: 2 }}
              />

              <Button
                type="submit"
                variant="contained"
                color="primary"
                fullWidth
                disabled={isSubmitting}
              >
                {isSubmitting ? "Registering..." : "Register"}
              </Button>
            </Form>
          )}
        </Formik>

        <Typography variant="body2" sx={{ mt: 2 }}>
          Already have an account?
          <Button variant="text" color="primary" onClick={() => navigate("/login")}>
            Sign In
          </Button>
        </Typography>
      </Box>
    </Container>
  );
};

export default Register;