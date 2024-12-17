import React from "react";
import { Box, TextField, Button, MenuItem, Typography } from "@mui/material";
import { Formik, Form, Field } from "formik";
import * as Yup from "yup";
import { useNavigate } from "react-router-dom";
import api from "../../api/api";

const CarListingSchema = Yup.object().shape({
  name: Yup.string()
    .required("Name is required.")
    .max(100, "Name cannot exceed 100 characters."),
  type: Yup.string()
    .required("Type is required.")
    .max(50, "Type cannot exceed 50 characters."),
  brand: Yup.string()
    .required("Brand is required.")
    .max(50, "Brand cannot exceed 50 characters."),
  price: Yup.number()
    .required("Price is required.")
    .min(1, "Price must be at least 1.")
    .max(100000000, "Price cannot exceed 100,000,000."),
  gearbox: Yup.string()
    .required("Gearbox is required.")
    .max(20, "Gearbox cannot exceed 20 characters."),
  state: Yup.string()
    .required("State is required.")
    .max(20, "State cannot exceed 20 characters."),
  km: Yup.number()
    .required("Kilometers driven is required.")
    .min(0, "Kilometers cannot be negative.")
    .max(1000000, "Kilometers cannot exceed 1,000,000."),
  productionYear: Yup.number()
    .required("Production Year is required.")
    .min(1900, "Year must be between 1900 and 2024.")
    .max(2024, "Year must be between 1900 and 2024."),
  horsepower: Yup.number()
    .required("Horsepower is required.")
    .min(1, "Horsepower must be at least 1.")
    .max(2000, "Horsepower cannot exceed 2000."),
  color: Yup.string()
    .required("Color is required.")
    .max(30, "Color cannot exceed 30 characters."),
  extraInfo: Yup.string().max(500, "Extra Info cannot exceed 500 characters."),
  imageURL: Yup.string().url("Image URL must be a valid URL."),
});

const AddCarListing = () => {
  const navigate = useNavigate();

  const handleSubmit = async (values, { setSubmitting, setErrors }) => {
    try {
      await api.post("/CarListing", values);
      navigate("/carlisting/list");
    } catch (error) {
      console.error(error);
      navigate('/error');
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <Formik
      initialValues={{
        name: "",
        type: "",
        brand: "",
        price: "",
        gearbox: "",
        state: "",
        km: "",
        productionYear: "",
        horsepower: "",
        color: "",
        extraInfo: "",
        imageURL: "",
      }}
      validationSchema={CarListingSchema}
      onSubmit={handleSubmit}
    >
      {({ errors, touched, isSubmitting }) => (
        <Box
          component={Form}
          sx={{
            maxWidth: 500,
            margin: "auto",
            display: "flex",
            flexDirection: "column",
            gap: 2,
            mb: "1em",
          }}
        >
          <Typography variant="h5" sx={{ textAlign: "center", mb: 2 }}>
            Add Car Listing
          </Typography>

          <Field
            as={TextField}
            label="Name"
            name="name"
            fullWidth
            error={touched.name && Boolean(errors.name)}
            helperText={touched.name && errors.name}
          />

          <Field
            as={TextField}
            label="Car Type"
            name="type"
            fullWidth
            select
            error={touched.type && Boolean(errors.type)}
            helperText={touched.type && errors.type}
          >
            <MenuItem value="SUV">SUV</MenuItem>
            <MenuItem value="Sedan">Sedan</MenuItem>
            <MenuItem value="Truck">Truck</MenuItem>
            <MenuItem value="Convertible">Convertible</MenuItem>
          </Field>

          <Field
            as={TextField}
            label="Car Brand"
            name="brand"
            fullWidth
            error={touched.brand && Boolean(errors.brand)}
            helperText={touched.brand && errors.brand}
          >
          </Field>

          <Field
            as={TextField}
            label="Price ($)"
            name="price"
            type="number"
            fullWidth
            error={touched.price && Boolean(errors.price)}
            helperText={touched.price && errors.price}
          />

          <Field
            as={TextField}
            label="Gearbox"
            name="gearbox"
            fullWidth
            select
            error={touched.gearbox && Boolean(errors.gearbox)}
            helperText={touched.gearbox && errors.gearbox}
          >
            <MenuItem value="Manual">Manual</MenuItem>
            <MenuItem value="Automatic">Automatic</MenuItem>
          </Field>

          <Field
            as={TextField}
            label="State"
            name="state"
            select
            fullWidth
            error={touched.state && Boolean(errors.state)}
            helperText={touched.state && errors.state}
          >
            <MenuItem value="New">New</MenuItem>
            <MenuItem value="Used">Used</MenuItem>
          </Field>

          <Field
            as={TextField}
            label="Kilometers (KM)"
            name="km"
            type="number"
            fullWidth
            error={touched.km && Boolean(errors.km)}
            helperText={touched.km && errors.km}
          />

          <Field
            as={TextField}
            label="Production Year"
            name="productionYear"
            type="number"
            fullWidth
            error={touched.productionYear && Boolean(errors.productionYear)}
            helperText={touched.productionYear && errors.productionYear}
          />

          <Field
            as={TextField}
            label="Horsepower"
            name="horsepower"
            type="number"
            fullWidth
            error={touched.horsepower && Boolean(errors.horsepower)}
            helperText={touched.horsepower && errors.horsepower}
          />

          <Field
            as={TextField}
            label="Color"
            name="color"
            fullWidth
            error={touched.color && Boolean(errors.color)}
            helperText={touched.color && errors.color}
          />

          <Field
            as={TextField}
            label="Extra Information"
            name="extraInfo"
            multiline
            rows={4}
            fullWidth
            error={touched.extraInfo && Boolean(errors.extraInfo)}
            helperText={touched.extraInfo && errors.extraInfo}
          />

          <Field
            as={TextField}
            label="Image URL"
            name="imageURL"
            fullWidth
            error={touched.imageURL && Boolean(errors.imageURL)}
            helperText={touched.imageURL && errors.imageURL}
          />

          <Button
            type="submit"
            variant="contained"
            color="primary"
            disabled={isSubmitting}
          >
            {isSubmitting ? "Submitting..." : "Submit"}
          </Button>
        </Box>
      )}
    </Formik>
  );
};

export default AddCarListing;